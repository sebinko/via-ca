import { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import type { StoryItem } from '../types';
import { getStoryItemById, deleteStoryItem } from '../api/storyApi';

const StoryDetail: React.FC = () => {
  const [story, setStory] = useState<StoryItem | null>(null);
  const [loading, setLoading] = useState(true);
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

  const handleDelete = async () => {
    if (!id || !window.confirm('Are you sure you want to delete this story?')) {
      return;
    }
    
    try {
      await deleteStoryItem(parseInt(id));
      navigate('/');
    } catch (err) {
      setError('Failed to delete the story');
      console.error('Error deleting story:', err);
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
    <div className="story-detail">
      <Link to="/" className="back-link">← Back to Stories</Link>
      
      <div className="story-header">
        <h1>{story.title}</h1>
        {story.category && <span className="category">{story.category}</span>}
      </div>
      
      <div className="story-meta">
        {story.author && <span className="author">By {story.author}</span>}
        <span className="date">Published on {new Date(story.createdAt).toLocaleDateString()}</span>
      </div>
      
      {story.imageUrl && (
        <div className="story-image">
          <img src={story.imageUrl} alt={story.title} />
        </div>
      )}
      
      <div className="story-content">
        {story.content.split('\n').map((paragraph, index) => (
          <p key={index}>{paragraph}</p>
        ))}
      </div>
      
      <div className="story-actions">
        <Link to={`/edit-story/${story.id}`} className="btn-edit">Edit Story</Link>
        <button onClick={handleDelete} className="btn-delete">Delete Story</button>
      </div>
    </div>
  );
};

const StoryDetailExport = StoryDetail;
export default StoryDetailExport;
