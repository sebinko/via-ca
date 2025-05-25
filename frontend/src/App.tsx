import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  const [message, setMessage] = useState('')
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    const fetchMessage = async () => {
      try {
        setLoading(true)
        const response = await fetch('https://localhost:7118/api/message') 
        if (!response.ok) {
          throw new Error('Network response was not ok')
        }
        const data = await response.json()
        setMessage(data.message)
      } catch (err) {
        setError('Failed to fetch message from backend: ' + (err instanceof Error ? err.message : String(err)))
      } finally {
        setLoading(false)
      }
    }

    fetchMessage()
  }, [])

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React + C# API</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        
        <div className="backend-message">
          <h3>Backend Connection:</h3>
          {loading ? (
            <p>Loading message from backend...</p>
          ) : error ? (
            <p className="error">{error}</p>
          ) : (
            <p className="success">Message: {message}</p>
          )}
        </div>
        
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}

export default App
