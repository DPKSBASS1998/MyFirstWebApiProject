import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { getProfile, updateProfile } from "../api/Profile";
import type { ProfileDto } from "../dto/profile/ProfileDto";

type ProfileState = {
  data: ProfileDto | null;
  loading: boolean;
};

const initialState: ProfileState = {
  data: null,
  loading: false,
};

export const fetchProfile = createAsyncThunk("profile/fetch", async () => {
  return await getProfile();
});

export const updateProfileThunk = createAsyncThunk(
  "profile/update",
  async (data: Partial<ProfileDto>, { dispatch }) => {
    await updateProfile(data);
    return await dispatch(fetchProfile()).unwrap();
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
        state.data = action.payload;
        state.loading = false;
      })
      .addCase(updateProfileThunk.rejected, (state) => {
        state.loading = false;
      });
  },
});

export default profileSlice.reducer;