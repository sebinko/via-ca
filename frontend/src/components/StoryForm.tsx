import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import type { StoryItemFormData } from '../types';
import { createStoryItem } from '../api/storyApi';

const initialFormState: StoryItemFormData = {
  title: '',
  content: '',
  imageUrl: '',
  author: '',
  category: '',
  isPublished: true,
};

const StoryForm: React.FC = () => {
  const [formData, setFormData] = useState<StoryItemFormData>(initialFormState);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    
    // Validate form
    if (!formData.title.trim() || !formData.content.trim()) {
      setError('Title and content are required');
      return;
    }
    
    setIsSubmitting(true);
    setError(null);
    
    try {
      await createStoryItem(formData);
      navigate('/');
    } catch (err) {
      setError('Failed to create story. Please try again later.');
      console.error('Error creating story:', err);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="story-form-container">
      <h2>Add New Story</h2>
      
      {error && <div className="error">{error}</div>}
      
      <form onSubmit={handleSubmit} className="story-form">
        <div className="form-group">
          <label htmlFor="title">Title*</label>
          <input
            type="text"
            id="title"
            name="title"
            value={formData.title}
            onChange={handleChange}
            required
            placeholder="Enter a catchy title"
          />
        </div>

        <div className="form-group">
          <label htmlFor="content">Content*</label>
          <textarea
            id="content"
            name="content"
            value={formData.content}
            onChange={handleChange}
            required
            rows={5}
            placeholder="Write your story content here"
          />
        </div>

        <div className="form-group">
          <label htmlFor="author">Author</label>
          <input
            type="text"
            id="author"
            name="author"
            value={formData.author}
            onChange={handleChange}
            placeholder="Who wrote this story?"
          />
        </div>

        <div className="form-group">
          <label htmlFor="category">Category</label>
          <select
            id="category"
            name="category"
            value={formData.category}
            onChange={handleChange}
          >
            <option value="">Select a category</option>
            <option value="Entertainment">Entertainment</option>
            <option value="Politics">Politics</option>
            <option value="Sports">Sports</option>
            <option value="Technology">Technology</option>
            <option value="Science">Science</option>
            <option value="Local News">Local News</option>
            <option value="World News">World News</option>
            <option value="Business">Business</option>
            <option value="Health">Health</option>
            <option value="Lifestyle">Lifestyle</option>
          </select>
        </div>

        <div className="form-group">
          <label htmlFor="imageUrl">Image URL</label>
          <input
            type="url"
            id="imageUrl"
            name="imageUrl"
            value={formData.imageUrl}
            onChange={handleChange}
            placeholder="https://example.com/image.jpg"
          />
        </div>

        <div className="form-group checkbox-group">
          <label>
            <input
              type="checkbox"
              name="isPublished"
              checked={formData.isPublished ?? true}
              onChange={(e) => setFormData(prev => ({
                ...prev,
                isPublished: e.target.checked
              }))}
            />
            Publish immediately
          </label>
        </div>

        <div className="form-actions">
          <button 
            type="button" 
            onClick={() => navigate('/')} 
            className="btn-cancel"
            disabled={isSubmitting}
          >
            Cancel
          </button>
          <button 
            type="submit" 
            className="btn-submit" 
            disabled={isSubmitting}
          >
            {isSubmitting ? 'Adding...' : 'Add Story'}
          </button>
        </div>
      </form>
    </div>
  );
};

const StoryFormExport = StoryForm;
export default StoryFormExport;
