import type { Product } from "./Product";

export interface Switches extends Product {
  type: string;
  operatingForce: number;
  totalTravel: number;
  preTravel: number;
  tactilePosition: number;
  tactileForce: number;
  imagePath: string;
  pinCount: number;
}
