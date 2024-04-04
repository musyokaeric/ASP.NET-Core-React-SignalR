import { useEffect, useState } from 'react'
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';
import { Activity } from '../models/activity';

function App() {

    const [activities, setActivities] = useState<Activity[]>([]);

    useEffect(() => {
        axios.get<Activity[]>('https://localhost:7000/api/activities')
            .then(response => setActivities(response.data))
            .catch(error => console.error(error))
    }, [])

    return (
        <>
            <Header as='h2' icon='users' content='Reactivities' />
            <List>
                {
                    activities.map((activity: Activity, index) => <List.Item key={index}>{activity.title}</List.Item>)
                }
            </List>
        </>
    )
}

export default App
