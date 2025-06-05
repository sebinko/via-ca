import { BrowserRouter, Routes, Route } from 'react-router-dom';
import StoryList from './components/StoryList';
import StoryForm from './components/StoryForm';
import StoryDetail from './components/StoryDetail';
import EditStoryForm from './components/EditStoryForm';
import Navbar from './components/Navbar';
import './App.css';

function App() {
  return (
    <BrowserRouter>
      <div className="app-container">
        <Navbar />
        <Routes>
          <Route path="/" element={<StoryList />} />
          <Route path="/add-story" element={<StoryForm />} />
          <Route path="/story/:id" element={<StoryDetail />} />
          <Route path="/edit-story/:id" element={<EditStoryForm />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
