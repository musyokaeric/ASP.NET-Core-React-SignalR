import { useEffect, useState } from 'react'
/*import './App.css'*/
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';

function App() {

    const [activities, setActivities] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:7000/api/activities')
            .then(response => setActivities(response.data))
            .catch(error => console.error(error))
    }, [])

    return (
        <>
            <Header as='h2' icon='users' content='Reactivities'/>
            <List>
                {
                    activities.map((activity: any, index) => <List.Item key={index}>{activity.title}</List.Item>)
                }
            </List>
        </>
    )
}

export default App
