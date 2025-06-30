// dto/SwitchFilterDto.ts
export interface SwitchFilterDto {
  type?: string;
  manufacturer?: string;
  minOperatingForce?: number;
  maxOperatingForce?: number;
  minTotalTravel?: number;
  maxTotalTravel?: number;
  minPreTravel?: number;
  maxPreTravel?: number;
  minTactilePosition?: number;
  maxTactilePosition?: number;
  minTactileForce?: number;
  maxTactileForce?: number;
  pinCount?: number;
  minPrice?: number;
  maxPrice?: number;
  inStock?: boolean;
}