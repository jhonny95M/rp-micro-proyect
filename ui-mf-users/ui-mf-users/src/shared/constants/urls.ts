import { buildApiUrl } from 'web-utilities'

export const buildMSUrl = (endpoint: string, params?: any | undefined): URL | null => {
  const baseUrl: any = process.env.TEST_APP_MICROSERVICE_BASE_URL;
  return buildApiUrl(baseUrl, endpoint, params);
};
