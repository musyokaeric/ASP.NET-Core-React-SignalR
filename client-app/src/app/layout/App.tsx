import { useEffect, useState } from 'react'
import axios from 'axios';
import { Container } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';

function App() {

    const [activities, setActivities] = useState<Activity[]>([]);

    useEffect(() => {
        axios.get<Activity[]>('https://localhost:7000/api/activities')
            .then(response => setActivities(response.data))
            .catch(error => console.error(error))
    }, [])

    return (
        <>
            <NavBar />

            <Container style={{ marginTop: '7em' }}>
                <ActivityDashboard activities={activities} />
            </Container>
        </>
    )
}

export default App
