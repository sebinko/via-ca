import { useState, useEffect, useMemo } from 'react';
import type { StoryItem } from '../types';
import { getStoryItems, deleteStoryItem } from '../api/storyApi';
import { Link } from 'react-router-dom';

const StoryList: React.FC = () => {
  const [stories, setStories] = useState<StoryItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [filter, setFilter] = useState<'all' | 'published' | 'unpublished'>('all');
  const [categoryFilter, setCategoryFilter] = useState<string>('');

  const fetchStories = async () => {
    try {
      setLoading(true);
      const data = await getStoryItems();
      setStories(data);
      setError(null);
    } catch (err) {
      setError('Failed to fetch stories');
      console.error('Error fetching stories:', err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchStories();
  }, []);

  // Filter stories based on publication status and category
  const filteredStories = useMemo(() => {
    return stories.filter(story => {
      // Filter by publication status
      if (filter === 'published' && !story.isPublished) return false;
      if (filter === 'unpublished' && story.isPublished) return false;
      
      // Filter by category if a category is selected
      if (categoryFilter && story.category !== categoryFilter) return false;
      
      return true;
    });
  }, [stories, filter, categoryFilter]);

  // Extract unique categories for the filter dropdown
  const categories = useMemo(() => {
    const uniqueCategories = new Set<string>();
    stories.forEach(story => {
      if (story.category) uniqueCategories.add(story.category);
    });
    return Array.from(uniqueCategories).sort();
  }, [stories]);

  const handleDelete = async (id: number) => {
    if (!window.confirm('Are you sure you want to delete this story?')) {
      return;
    }
    
    try {
      await deleteStoryItem(id);
      setStories(stories.filter(story => story.id !== id));
    } catch (err) {
      setError('Failed to delete the story');
      console.error('Error deleting story:', err);
    }
  };

  if (loading) {
    return <div className="loading">Loading stories...</div>;
  }

  if (error) {
    return <div className="error">{error}</div>;
  }

  if (stories.length === 0) {
    return (
      <div>
        <h2>Tabloid Stories</h2>
        <p>No stories available. Be the first to add one!</p>
        <Link to="/add-story" className="btn">Add New Story</Link>
      </div>
    );
  }

  return (
    <div className="story-list">
      <div className="header">
        <h2>Tabloid Stories</h2>
        <Link to="/add-story" className="btn">Add New Story</Link>
      </div>

      <div className="story-filters">
        <div className="filter-group">
          <label htmlFor="statusFilter">Status:</label>
          <select 
            id="statusFilter" 
            value={filter} 
            onChange={(e) => setFilter(e.target.value as 'all' | 'published' | 'unpublished')}
          >
            <option value="all">All Stories</option>
            <option value="published">Published Only</option>
            <option value="unpublished">Unpublished Only</option>
          </select>
        </div>

        <div className="filter-group">
          <label htmlFor="categoryFilter">Category:</label>
          <select 
            id="categoryFilter" 
            value={categoryFilter} 
            onChange={(e) => setCategoryFilter(e.target.value)}
          >
            <option value="">All Categories</option>
            {categories.map(category => (
              <option key={category} value={category}>{category}</option>
            ))}
          </select>
        </div>
      </div>

      <div className="stories">
        {filteredStories.map(story => (
          <div key={story.id} className="story-card">
            <div className="story-header">
              <h3>
                {story.title}
                {!story.isPublished && <span className="unpublished-tag">Unpublished</span>}
              </h3>
              {story.category && <span className="category">{story.category}</span>}
            </div>
            
            <p className="story-content">{story.content.length > 150 
              ? `${story.content.substring(0, 150)}...` 
              : story.content}
            </p>
            
            <div className="story-footer">
              <div className="story-meta">
                {story.author && <span className="author">By {story.author}</span>}
                <span className="date">{new Date(story.createdAt).toLocaleDateString()}</span>
              </div>
              
              <div className="story-actions">
                <Link to={`/story/${story.id}`} className="btn-view">View</Link>
                <Link to={`/edit-story/${story.id}`} className="btn-edit">Edit</Link>
                <button 
                  onClick={() => handleDelete(story.id)} 
                  className="btn-delete">
                  Delete
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

const StoryListExport = StoryList;
export default StoryListExport;
