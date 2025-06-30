import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { getMe, loginApi, registerApi, logoutApi } from "../api/Auth";
import type { LoginDto } from "../dto/auth/LoginDto";
import type { RegistrationDto } from "../dto/auth/RegistrationDto";

type AuthState = {
  user: number | null;
  isAuthenticated: boolean;
  loading: boolean;
};

const initialState: AuthState = {
  user: null,
  isAuthenticated: false,
  loading: true,
};

export const fetchMe = createAsyncThunk("auth/me", async () => {
  return await getMe();
});

export const login = createAsyncThunk("auth/login", async (data: LoginDto) => {
  await loginApi(data);
  return await getMe();
});

export const register = createAsyncThunk("auth/register", async (data: RegistrationDto) => {
  await registerApi(data);
  return await getMe();
});

export const logout = createAsyncThunk("auth/logout", async () => {
  await logoutApi();
});

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchMe.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchMe.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload.id ?? null;
        state.isAuthenticated = !!action.payload.isAuthenticated;
      })
      .addCase(fetchMe.rejected, (state) => {
        state.loading = false;
        state.user = null;
        state.isAuthenticated = false;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.user = action.payload.id ?? null;
        state.isAuthenticated = !!action.payload.isAuthenticated;
      })
      .addCase(register.fulfilled, (state, action) => {
        state.user = action.payload.id ?? null;
        state.isAuthenticated = !!action.payload.isAuthenticated;
      })
      .addCase(logout.fulfilled, (state) => {
        state.user = null;
        state.isAuthenticated = false;
      });
  },
});

export default authSlice.reducer;