import type { LoginDto } from "../auth/LoginDto";
import type { RegisterDto } from "../auth/RegisterDto";
import type { ProfileDto } from "./ProfileDto";
import type { AddressDto } from "../address/AddressDto";

export interface UserProfileContextType {
  // --- Авторизація ---
  user: string | null;
  isAuthenticated: boolean;
  loading: boolean;
  login: (data: LoginDto) => Promise<void>;
  register: (data: RegisterDto) => Promise<void>;
  logout: () => Promise<void>;

  // --- Профіль ---
  profile: {
    data: ProfileDto | null;
    loading: boolean;
    fetch: () => Promise<void>;
    update: (data: Partial<ProfileDto>) => Promise<void>;
  };

  // --- Адреси ---
  addresses: {
    list: AddressDto[];
    loading: boolean;
    fetch: () => Promise<void>;
    add: (address: AddressDto) => Promise<void>;
    update: (address: AddressDto) => Promise<void>;
    remove: (id: number) => Promise<void>;
    select: (id: number) => void;
    selectedId: number | null;
  };
}