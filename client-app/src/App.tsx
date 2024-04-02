import { useEffect, useState } from 'react'
import './App.css'
import axios from 'axios';

function App() {

    const [activities, setActivities] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:7000/api/activities')
            .then(response => setActivities(response.data))
            .catch(error => console.error(error))
    }, [])

    return (
        <>
            <h1>Reactivities</h1>
            <ul>
                {
                    activities.map((activity: any, index) => <li key={index}>{activity.title}</li>)
                }
            </ul>
        </>
    )
}

export default App
