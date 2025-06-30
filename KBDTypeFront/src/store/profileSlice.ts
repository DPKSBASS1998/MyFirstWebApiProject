import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { getProfile, updateProfile } from "../api/Profile";
import type { UserProfileDto } from "../dto/profile/UserProfileDto";

type ProfileState = {
  data: UserProfileDto | null;
  loading: boolean;
};

const initialState: ProfileState = {
  data: null,
  loading: false,
};

export const fetchProfile = createAsyncThunk<UserProfileDto>("profile/fetch", 
  async () => {
  return await getProfile();
});

export const updateProfileThunk = createAsyncThunk(
  "profile/update",
  async (data: Partial<UserProfileDto>) => {
    // updateProfile повертає оновлений профіль
    return await updateProfile(data);
  }
);

const profileSlice = createSlice({
  name: "profile",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchProfile.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchProfile.fulfilled, (state, action) => {
        state.data = action.payload;
        state.loading = false;
      })
      .addCase(fetchProfile.rejected, (state) => {
        state.data = null;
        state.loading = false;
      })
      .addCase(updateProfileThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(updateProfileThunk.fulfilled, (state, action) => {
        state.data = action.payload; // тут payload — це вже оновлений профіль
        state.loading = false;
      })
      .addCase(updateProfileThunk.rejected, (state) => {
        state.loading = false;
      });
  },
});

export default profileSlice.reducer;