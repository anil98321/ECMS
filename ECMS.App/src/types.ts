// src/types.ts
export interface CampaignSummary {
  campaignId: number;
  campaignName: string;
  subject?: string;
  status: number;
  scheduledDate: string;
  totalClients: number;
  sentCount: number;
  failedCount: number;
  pendingCount?: number;
  groupNames?: string[];
  groupIds?: number[];
  htmlBody?: string;
  failedClients?: FailedClient[]; 
}

export interface CampaignFormData {
  id?: number;
  name: string;
  subject: string;
  htmlBody: string;
  scheduledDate: string;
  groupIds: number[];
}

export interface FailedClient {
  clientId: number;
  clientName: string;
  email: string;
  errorMessage: string;
}

export interface Group {
  id: number;
  name: string;
}
export interface FailedClient {
  clientId: number;
  clientName: string;
  email: string;
  errorMessage: string;
}
