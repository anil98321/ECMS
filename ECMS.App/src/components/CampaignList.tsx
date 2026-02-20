import { useState, useEffect } from 'react';
import {
  Container, Paper, Table, TableBody, TableCell, TableContainer,
  TableHead, TableRow, Button, IconButton, Chip, Toolbar, Typography, CircularProgress
} from '@mui/material';
import { Edit, Delete, Visibility, Add } from '@mui/icons-material';
import AddToQueueIcon from '@mui/icons-material/AddToQueue';
import CampaignForm from './CampaignForm';
import CampaignDetails from './CampaignDetails';
import { campaignApi } from '../services/api';
import type { CampaignSummary } from '../types';

const statusLabels: Record<number, string> = {
  0: 'Draft', 1: 'Queued', 2: 'Processing', 3: 'Completed'
};

const statusColors: Record<number, 'default' | 'primary' | 'secondary' | 'success' | 'warning'> = {
  0: 'default', 1: 'warning', 2: 'primary', 3: 'success'
};

export default function CampaignList() {
  const [campaigns, setCampaigns] = useState<CampaignSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editingCampaign, setEditingCampaign] = useState<CampaignSummary | null>(null);
  const [selectedCampaign, setSelectedCampaign] = useState<CampaignSummary | null>(null);

  const loadCampaigns = async (): Promise<void> => {
    setLoading(true);
    try {
      const data = await campaignApi.getCampaigns();
      setCampaigns(data.campaigns);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadCampaigns();
  }, []);

  const handleAction = async (action: 'edit' | 'delete' | 'queue', campaign: CampaignSummary) => {
    if (action === 'edit') {
      setEditingCampaign(campaign);
      setShowForm(true);
    } else if (action === 'delete') {
      if (window.confirm('Delete this campaign?')) {
        await campaignApi.deleteCampaign(campaign.campaignId);
        loadCampaigns();
      }
    } else if (action === 'queue') {
      await campaignApi.queueCampaign(campaign.campaignId);
      loadCampaigns();
    }
  };

  const handleSave = async () => {
    setShowForm(false);
    setEditingCampaign(null);
    loadCampaigns();
  };

  return (
    <Container maxWidth="lg" sx={{ py: 4 }}>
      <Paper elevation={1} sx={{ p: 3, mb: 4 }}>
        <Toolbar disableGutters>
          <Typography variant="h4" component="h1" sx={{ flexGrow: 1 }}>
            Email Campaigns Management System
          </Typography>
          <Button
            variant="contained"
            startIcon={<Add />}
            onClick={() => setShowForm(true)}
          >
            New Campaign
          </Button>
        </Toolbar>
      </Paper>

      {showForm && (
        <CampaignForm
          campaign={editingCampaign}
          onSave={handleSave}
          onClose={() => {
            setShowForm(false);
            setEditingCampaign(null);
          }}
        />
      )}

      <TableContainer component={Paper} elevation={2}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell sx={{ fontWeight: 'bold' }}>Name</TableCell>
              <TableCell sx={{ fontWeight: 'bold' }}>Subject</TableCell>
              <TableCell sx={{ fontWeight: 'bold' }}>Status</TableCell>
              <TableCell sx={{ fontWeight: 'bold' }}>Progress</TableCell>
              <TableCell sx={{ fontWeight: 'bold' }}>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {loading ? (
              <TableRow>
                <TableCell colSpan={5} align="center">
                  <CircularProgress />
                </TableCell>
              </TableRow>
            ) : campaigns.length === 0 ? (
              <TableRow>
                <TableCell colSpan={5} align="center">
                  No campaigns found
                </TableCell>
              </TableRow>
            ) : (
               campaigns?.map((campaign) => (
                <TableRow key={campaign.campaignId} hover>
                  <TableCell>
                    <Typography variant="body1" fontWeight="medium">
                      {campaign.campaignName}
                    </Typography>
                  </TableCell>
                  <TableCell>{campaign.subject || '-'}</TableCell>
                  <TableCell>
                    <Chip
                      label={statusLabels[campaign.status]}
                      color={statusColors[campaign.status]}
                      size="small"
                      variant="outlined"
                    />
                  </TableCell>
                  <TableCell>
                    <Typography variant="body2">
                      {campaign.sentCount + campaign.failedCount}/{campaign.totalClients}
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <div>
                      {campaign.status === 0 && (
                        <>
                          <IconButton
                            size="small"
                            onClick={() => handleAction('edit', campaign)}
                            title="Edit"
                          >
                            <Edit fontSize="small" />
                          </IconButton>
                          <IconButton
                            size="small"
                            onClick={() => handleAction('delete', campaign)}
                            title="Delete"
                            color="error"
                          >
                            <Delete fontSize="small" />
                          </IconButton>
                          <IconButton
                            size="small"
                            onClick={() => handleAction('queue', campaign)}
                            title="Queue"
                            color="success"
                          >
                            <AddToQueueIcon fontSize="small" />
                          </IconButton>
                        </>
                      )}
                      <IconButton
                        size="small"
                        onClick={() => setSelectedCampaign(campaign)}
                        title="View Details"
                      >
                        <Visibility fontSize="small" />
                      </IconButton>
                    </div>
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </TableContainer>

      {selectedCampaign && (
        <CampaignDetails
          campaign={selectedCampaign}
          onClose={() => setSelectedCampaign(null)}
        />
      )}
    </Container>
  );
}
