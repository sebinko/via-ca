import { Link } from 'react-router-dom';

const Navbar: React.FC = () => {
  return (
    <nav className="navbar">
      <div className="navbar-brand">
        <Link to="/">Tabloid App</Link>
      </div>
      <ul className="navbar-menu">
        <li className="navbar-item">
          <Link to="/">Home</Link>
        </li>
        <li className="navbar-item">
          <Link to="/add-story">Add Story</Link>
        </li>
      </ul>
    </nav>
  );
};

const NavbarExport = Navbar;
export default NavbarExport;
