import React from 'react';
import {
  Dialog, DialogTitle, DialogContent, IconButton,
  Typography, Chip, LinearProgress, Table, TableBody,
  TableCell, TableContainer, TableHead, TableRow, Paper,
  Box, Grid, Card, CardContent
} from '@mui/material';
import { Close } from '@mui/icons-material';
import type { CampaignSummary } from '../types';

interface Props {
  campaign: CampaignSummary;
  onClose: () => void;
}

export default function CampaignDetails({ campaign, onClose }: Props) {
  const progress = campaign.totalClients > 0 
    ? ((campaign.sentCount + campaign.failedCount) / campaign.totalClients) * 100 
    : 0;

  return (
    <Dialog open maxWidth="lg" fullWidth>
      <DialogTitle sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <Typography variant="h5">{campaign.campaignName}</Typography>
        <IconButton onClick={onClose}>
          <Close />
        </IconButton>
      </DialogTitle>
      
      <DialogContent>
        <Grid container spacing={3}>
          <Grid>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>Campaign Info</Typography>
                <Box sx={{ mb: 2 }}>
                  <Typography variant="body2" color="text.secondary">Subject:</Typography>
                  <Typography>{campaign.subject || '-'}</Typography>
                </Box>
                <Box>
                  <Typography variant="body2" color="text.secondary">Scheduled:</Typography>
                  <Typography>{new Date(campaign.scheduledDate).toLocaleString()}</Typography>
                </Box>
              </CardContent>
            </Card>
          </Grid>

          <Grid>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>Statistics</Typography>
                <Grid container spacing={2}>
                  <Grid>
                    <Typography variant="body2" color="text.secondary">Total Clients</Typography>
                    <Typography variant="h4">{campaign.totalClients}</Typography>
                  </Grid>
                  <Grid>
                    <Typography variant="body2" color="text.secondary">Sent</Typography>
                    <Typography variant="h4" color="success.main">{campaign.sentCount}</Typography>
                  </Grid>
                  <Grid>
                    <Typography variant="body2" color="text.secondary">Pending</Typography>
                    <Typography variant="h4">{campaign.pendingCount}</Typography>
                  </Grid>
                  <Grid >
                    <Typography variant="body2" color="text.secondary">Failed</Typography>
                    <Typography variant="h4" color="error.main">{campaign.failedCount}</Typography>
                  </Grid>
                </Grid>
              </CardContent>
            </Card>
          </Grid>

          <Grid>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom sx={{ mb: 2 }}>
                  Progress: {Math.round(progress)}%
                </Typography>
                <LinearProgress variant="determinate" value={progress} sx={{ height: 12, borderRadius: 6 }} />
              </CardContent>
            </Card>
          </Grid>

          {campaign.groupNames && campaign.groupNames?.length > 0 && (
            <Grid>
              <Card>
                <CardContent>
                  <Typography variant="h6" gutterBottom>Groups ({campaign.groupNames.length})</Typography>
                  <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 1 }}>
                    {campaign.groupNames.map((name, index) => (
                      <Chip key={index} label={name} variant="outlined" />
                    ))}
                  </Box>
                </CardContent>
              </Card>
            </Grid>
          )}

          {campaign.failedClients && campaign.failedClients?.length > 0 && (
            <Grid>
              <Card>
                <CardContent>
                  <Typography variant="h6" gutterBottom sx={{ mb: 2 }}>
                    Failed Clients ({campaign.failedClients.length})
                  </Typography>
                  <TableContainer component={Paper}>
                    <Table size="small">
                      <TableHead>
                        <TableRow>
                          <TableCell>Name</TableCell>
                          <TableCell>Email</TableCell>
                          <TableCell>Error</TableCell>
                        </TableRow>
                      </TableHead>
                      <TableBody>
                        {campaign.failedClients.map((client, index) => (
                          <TableRow key={index} hover>
                            <TableCell>{client.clientName}</TableCell>
                            <TableCell>{client.email}</TableCell>
                            <TableCell>{client.errorMessage}</TableCell>
                          </TableRow>
                        ))}
                      </TableBody>
                    </Table>
                  </TableContainer>
                </CardContent>
              </Card>
            </Grid>
          )}
        </Grid>
      </DialogContent>
    </Dialog>
  );
}
