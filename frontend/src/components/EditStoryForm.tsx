import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import type { StoryItem } from '../types';
import { getStoryItemById, updateStoryItem } from '../api/storyApi';

const EditStoryForm: React.FC = () => {
  const [story, setStory] = useState<StoryItem | null>(null);
  const [loading, setLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchStory = async () => {
      if (!id) return;
      
      try {
        setLoading(true);
        const data = await getStoryItemById(parseInt(id));
        setStory(data);
        setError(null);
      } catch (err) {
        setError('Failed to fetch story details');
        console.error('Error fetching story:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchStory();
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    if (!story) return;
    
    setStory({
      ...story,
      [name]: value,
    });
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    
    if (!story || !id) return;
    
    // Validate form
    if (!story.title.trim() || !story.content.trim()) {
      setError('Title and content are required');
      return;
    }
    
    setIsSubmitting(true);
    setError(null);
    
    try {
      await updateStoryItem(parseInt(id), story);
      navigate('/');
    } catch (err) {
      setError('Failed to update story. Please try again later.');
      console.error('Error updating story:', err);
    } finally {
      setIsSubmitting(false);
    }
  };

  if (loading) {
    return <div className="loading">Loading story details...</div>;
  }

  if (error) {
    return <div className="error">{error}</div>;
  }

  if (!story) {
    return <div className="error">Story not found</div>;
  }

  return (
    <div className="story-form-container">
      <h2>Edit Story</h2>
      
      {error && <div className="error">{error}</div>}
      
      <form onSubmit={handleSubmit} className="story-form">
        <div className="form-group">
          <label htmlFor="title">Title*</label>
          <input
            type="text"
            id="title"
            name="title"
            value={story.title}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="content">Content*</label>
          <textarea
            id="content"
            name="content"
            value={story.content}
            onChange={handleChange}
            required
            rows={5}
          />
        </div>

        <div className="form-group">
          <label htmlFor="author">Author</label>
          <input
            type="text"
            id="author"
            name="author"
            value={story.author || ''}
            onChange={handleChange}
          />
        </div>

        <div className="form-group">
          <label htmlFor="category">Category</label>
          <select
            id="category"
            name="category"
            value={story.category || ''}
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
            value={story.imageUrl || ''}
            onChange={handleChange}
          />
        </div>

        <div className="form-group checkbox-group">
          <label>
            <input
              type="checkbox"
              name="isPublished"
              checked={story.isPublished}
              onChange={(e) => setStory({
                ...story,
                isPublished: e.target.checked
              })}
            />
            Published
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
            {isSubmitting ? 'Saving...' : 'Save Changes'}
          </button>
        </div>
      </form>
    </div>
  );
};

const EditStoryFormExport = EditStoryForm;
export default EditStoryFormExport;
