import { createSlice, createAsyncThunk, type PayloadAction } from "@reduxjs/toolkit";
import { getAddresses, addAddress, updateAddress, removeAddress } from "../api/Address";
import type { AddressDto } from "../dto/address/AddressDto";

type AddressesState = {
  list: AddressDto[];
  loading: boolean;
  selectedId: number | null;
};

const initialState: AddressesState = {
  list: [],
  loading: false,
  selectedId: null,
};

export const fetchAddresses = createAsyncThunk<AddressDto[]>("addresses/fetch", async () => {
  return await getAddresses();
});

export const addAddressThunk = createAsyncThunk(
  "addresses/add", // name the thunk for clarity
  async (address: AddressDto, { dispatch }) => {
    await addAddress(address);
    return await dispatch(fetchAddresses()).unwrap();
  }
);

export const updateAddressThunk = createAsyncThunk(
  "addresses/update",
  async (address: AddressDto, { dispatch }) => {
    await updateAddress(address);
    return await dispatch(fetchAddresses()).unwrap();
  }
);

export const removeAddressThunk = createAsyncThunk(
  "addresses/remove",
  async (id: number, { dispatch }) => {
    await removeAddress(id);
    return await dispatch(fetchAddresses()).unwrap();
  }
);

const addressesSlice = createSlice({
  name: "addresses",
  initialState,
  reducers: {
    selectAddress(state, action: PayloadAction<number>) {
      state.selectedId = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchAddresses.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchAddresses.fulfilled, (state, action) => {
        state.list = action.payload;
        state.loading = false;
      })
      .addCase(fetchAddresses.rejected, (state) => {
        state.loading = false;
      })
      .addCase(addAddressThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(addAddressThunk.fulfilled, (state, action) => {
        state.list = action.payload;
        state.loading = false;
      })
      .addCase(updateAddressThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(updateAddressThunk.fulfilled, (state, action) => {
        state.list = action.payload;
        state.loading = false;
      })
      .addCase(removeAddressThunk.pending, (state) => {
        state.loading = true;
      })
      .addCase(removeAddressThunk.fulfilled, (state, action) => {
        state.list = action.payload;
        state.loading = false;
      });
  },
});

export const { selectAddress } = addressesSlice.actions;
export default addressesSlice.reducer;