export interface StoryItem {
  id: number;
  title: string;
  content: string;
  imageUrl?: string;
  createdAt: string;
  author?: string;
  isPublished: boolean;
  category?: string;
}

export interface StoryItemFormData {
  title: string;
  content: string;
  imageUrl?: string;
  author?: string;
  isPublished?: boolean;
  category?: string;
}
