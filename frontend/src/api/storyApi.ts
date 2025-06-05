import axios from 'axios';
import type { StoryItem, StoryItemFormData } from '../types';
import config from '../config';

const API_URL = `${config.apiBaseUrl}/StoryItems`;

export const getStoryItems = async () => {
  const response = await axios.get<StoryItem[]>(API_URL);
  return response.data;
};

export const getStoryItemById = async (id: number) => {
  const response = await axios.get<StoryItem>(`${API_URL}/${id}`);
  return response.data;
};

export const createStoryItem = async (storyItem: StoryItemFormData) => {
  const response = await axios.post<StoryItem>(API_URL, storyItem);
  return response.data;
};

export const updateStoryItem = async (id: number, storyItem: StoryItem) => {
  const response = await axios.put<StoryItem>(`${API_URL}/${id}`, storyItem);
  return response.data;
};

export const deleteStoryItem = async (id: number) => {
  await axios.delete(`${API_URL}/${id}`);
};
