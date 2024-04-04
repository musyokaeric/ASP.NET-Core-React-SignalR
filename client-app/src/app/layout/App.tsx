import { useEffect, useState } from 'react'
import axios from 'axios';
import { Container, List } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';

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
                <List>
                    {
                        activities.map((activity: Activity, index) => <List.Item key={index}>{activity.title}</List.Item>)
                    }
                </List>
            </Container>
        </>
    )
}

export default App
