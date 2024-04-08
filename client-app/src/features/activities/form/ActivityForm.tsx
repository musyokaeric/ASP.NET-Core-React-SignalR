import { Button, Form, Segment } from "semantic-ui-react";
import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { Link, useNavigate, useParams } from "react-router-dom";
import { Activity } from "../../../app/models/activity";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { v4 as uuid } from 'uuid';

export default observer(function ActivityForm() {

    const { activityStore } = useStore();
    const { createActivity, updateActivity, loading, loadActivity, loadingInitial } = activityStore;

    const { id } = useParams();

    const navigate = useNavigate();

    const [activity, setActivity] = useState<Activity>({
        id: '',
        title: '',
        category: '',
        description: '',
        date: '',
        city: '',
        venue: ''
    })

    useEffect(() => {
        if (id) loadActivity(id)
            .then(activity => setActivity(activity!));
    }, [id, loadActivity])

    function handleSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault();

        if (!activity.id) {
            activity.id = uuid();
            createActivity(activity).then(() => navigate(`/activities/${activity.id}`));
        }
        else {
            updateActivity(activity).then(() => navigate(`/activities/${activity.id}`));
        }
    }

    function inputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target;
        setActivity({
            ...activity,
            [name]: value
        })
    }

    if(loadingInitial) return <LoadingComponent content='Loading activity...'/>

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input placeholder='Title' value={activity.title} name='title' onChange={inputChange} />
                <Form.TextArea placeholder='Description' value={activity.description} name='description' onChange={inputChange} />
                <Form.Input placeholder='Category' value={activity.category} name='category' onChange={inputChange} />
                <Form.Input type='date' placeholder='Date' value={activity.date} name='date' onChange={inputChange} />
                <Form.Input placeholder='City' value={activity.city} name='city' onChange={inputChange} />
                <Form.Input placeholder='Venue' value={activity.venue} name='venue' onChange={inputChange} />

                <Button loading={loading} floated='right' positive type='submit' content='Submit' />
                <Button as={Link} to={`/activities`} floated='right' type='button' content='Cancel' />
            </Form>
        </Segment>
    )
})