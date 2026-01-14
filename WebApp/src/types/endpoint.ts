export type FieldType = "String" | "Number" | "Boolean";

export interface EndpointField {
  name: string;
  type: FieldType;
  isRequired: boolean;
}

export interface EndpointSchema {
  fields: EndpointField[];
}

export interface RateLimit {
  requestsPerSecond: number;
}

export interface AdminEndpoint {
  id?: string;
  name: string;
  description: string;
  path: string;
  status: "Enabled" | "Disabled";
  protocols: number | string;
  rateLimit: RateLimit;
  schema: EndpointSchema;
}