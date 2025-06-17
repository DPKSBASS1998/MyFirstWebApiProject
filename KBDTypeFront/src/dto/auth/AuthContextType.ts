import type { LoginDto } from "./LoginDto";
import type { RegisterDto } from "./RegisterDto";

export interface AuthContextType {
  user: string | null;
  isAuthenticated: boolean;
  loading: boolean;
  login: (data: LoginDto) => Promise<void>;
  register: (data: RegisterDto) => Promise<void>;
  logout: () => Promise<void>;
}
