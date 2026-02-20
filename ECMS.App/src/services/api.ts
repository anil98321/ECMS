// src/services/api.js - COMPLETE VERSION
const API_BASE = 'https://localhost:7025/api/v1'; // Your .NET API

const handleResponse = async (response) => {
  if (!response.ok) {
    const errorData = await response.json().catch(() => ({}));
    throw new Error(errorData.message || `HTTP ${response.status}`);
  }
  return response.json();
};

export const campaignApi = {
  getCampaigns: () => fetch(`${API_BASE}/campaigns`).then(handleResponse),
  
  saveCampaign: async (campaign) => {
    const method = campaign.id ? 'PUT' : 'POST';
    const url = campaign.id ? `${API_BASE}/campaigns/${campaign.id}` : `${API_BASE}/campaigns`;
    const payload = {
      campaignName: campaign.name,
      subject: campaign.subject,
      htmlBody: campaign.htmlBody,
      scheduledDate: campaign.scheduledDate,
      groups: campaign.groupIds,
      ...(campaign.id && { CampaignId: campaign.id })  
    };
    const response = await fetch(url, {
      method,
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    });
    return handleResponse(response);
  },
  
  deleteCampaign: (id) => fetch(`${API_BASE}/campaigns/${id}`, { method: 'DELETE' }).then(handleResponse),
  queueCampaign: (id) => fetch(`${API_BASE}/campaigns/${id}/queue`, { method: 'POST' }).then(handleResponse),
  getCampaignDetails: (id) => fetch(`${API_BASE}/campaigns/${id}/details`).then(handleResponse)
};

// ✅ ADD THIS: groupApi
export const groupApi = {
  // getGroups: () => fetch(`${API_BASE}/groups`).then(handleResponse)
  getGroups : () => { 
    const groups = [
    {
    id: 1, 
    name: 'VIP Customers',
    IsActive: 1
    },
     {
    id: 2, 
    name: 'Newsletter Subscribers',
    IsActive: 1
    },
    {
    id: 3, 
    name: 'High Value Customers',
    IsActive: 1
    },
     {
    id: 4, 
    name: 'Abandoned Cart',
    IsActive: 1
    },
    {
    id: 5, 
    name: 'New Subscribers',
    IsActive: 1
    },
    {
    id: 6, 
    name: 'Loyal Customers',
    IsActive: 1
    }
  ]
    return groups; }
};
