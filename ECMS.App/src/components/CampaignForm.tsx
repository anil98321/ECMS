import { useState, useEffect } from 'react';
import {
  Dialog, DialogTitle, DialogContent, DialogActions,
  TextField, Button, FormControlLabel, Checkbox, FormGroup,
  FormHelperText, FormControl, 
  Box, CircularProgress, Typography
} from '@mui/material';
import { Save, Close } from '@mui/icons-material';
import { campaignApi, groupApi } from '../services/api';
import type { CampaignFormData, CampaignSummary } from '../types';

interface Props {
  campaign?: CampaignSummary | null;
  onSave: () => void;
  onClose: () => void;
}

export default function CampaignForm({ campaign, onSave, onClose }: Props) {
  const [formData, setFormData] = useState<CampaignFormData>({
    name: '',
    subject: '',
    htmlBody: '',
    scheduledDate: '',
    groupIds: []
  });
  const [groups, setGroups] = useState<{ id: number; name: string }[]>([]);
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [loading, setLoading] = useState(false);
  const [groupsLoading, setGroupsLoading] = useState(true);

  useEffect(() => {
    loadGroups();
    if (campaign) {
      setFormData({
        name: campaign.campaignName || '',
        subject: campaign.subject || '',
        htmlBody: campaign.htmlBody || '',
        scheduledDate: campaign.scheduledDate ? new Date(campaign.scheduledDate).toISOString().slice(0, 16) : '',
        groupIds: campaign.groupIds || [],
        ...(campaign && { id: campaign.campaignId })
      });
    }
  }, [campaign]);

  const loadGroups = async () => {
    try {
      const data = await groupApi.getGroups();
      setGroups(data);
    } finally {
      setGroupsLoading(false);
    }
  };

  const validate = (): boolean => {
    const newErrors: Record<string, string> = {};
    if (!formData.name.trim()) newErrors.name = 'Campaign name is required';
    if (!formData.subject.trim()) newErrors.subject = 'Subject is required';
    if (!formData.htmlBody.trim()) newErrors.htmlBody = 'HTML body is required';
    if (!formData.scheduledDate) newErrors.scheduledDate = 'Scheduled date is required';
    if (formData.groupIds.length === 0) newErrors.groups = 'Select at least one group';
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async () => {
    if (!validate()) return;
    
    setLoading(true);
    try {
      await campaignApi.saveCampaign(formData);
      onSave();
    } finally {
      setLoading(false);
    }
  };

  const handleGroupToggle = (groupId: number) => {
    setFormData(prev => ({
      ...prev,
      groupIds: prev.groupIds.includes(groupId)
        ? prev.groupIds.filter(id => id !== groupId)
        : [...prev.groupIds, groupId]
    }));
  };

  return (
    <Dialog open maxWidth="md" fullWidth>
      <DialogTitle>
        {campaign ? 'Edit Campaign' : 'New Campaign'}
      </DialogTitle>
      <DialogContent>
        <Box component="form" sx={{ '& > * + *': { mt: 2 } }} noValidate>
          <TextField
            fullWidth
            label="Campaign Name *"
            value={formData.name}
            onChange={(e) => setFormData({ ...formData, name: e.target.value })}
            error={!!errors.name}
            helperText={errors.name}
            required
            sx={{ mb: 2 }}
          />
          <TextField
            fullWidth
            label="Subject *"
            value={formData.subject}
            onChange={(e) => setFormData({ ...formData, subject: e.target.value })}
            error={!!errors.subject}
            helperText={errors.subject}
            required
            sx={{ mb: 2 }}
          />

          <TextField
            fullWidth
            label="HTML Body *"
            multiline
            rows={6}
            value={formData.htmlBody}
            onChange={(e) => setFormData({ ...formData, htmlBody: e.target.value })}
            error={!!errors.htmlBody}
            helperText={errors.htmlBody}
            required
            sx={{ mb: 2 }}
          />

          <TextField
            fullWidth
            label="Scheduled Date *"
            type="datetime-local"
            InputLabelProps={{ shrink: true }}
            value={formData.scheduledDate}
            onChange={(e) => setFormData({ ...formData, scheduledDate: e.target.value })}
            error={!!errors.scheduledDate}
            helperText={errors.scheduledDate}
            required
            sx={{ mb: 2 }}
          />

          <FormControl fullWidth error={!!errors.groups} required>          
             <Typography variant="subtitle1" gutterBottom sx={{ fontWeight: 500 }}>
    Select Groups <span style={{ color: 'red' }}>*</span>
  </Typography>
            <FormGroup>
              {groupsLoading ? (
                <CircularProgress size={20} />
              ) : (
                groups.map((group) => (
                  <FormControlLabel
                    key={group.id}
                    control={
                      <Checkbox
                        checked={formData.groupIds.includes(group.id)}
                        onChange={() => handleGroupToggle(group.id)}
                      />
                    }
                    label={group.name}
                  />
                ))
              )}
            </FormGroup>
            {errors.groups && <FormHelperText>{errors.groups}</FormHelperText>}
          </FormControl>
        </Box>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} disabled={loading}>
          <Close fontSize="small" sx={{ mr: 1 }} />
          Cancel
        </Button>
        <Button
          onClick={handleSubmit}
          variant="contained"
          startIcon={<Save />}
          disabled={loading}
        >
          {loading ? <CircularProgress size={20} /> : 'Save'}
        </Button>
      </DialogActions>
    </Dialog>
  );
}
