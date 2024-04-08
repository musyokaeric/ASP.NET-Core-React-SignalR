import { Button, Form, Segment } from "semantic-ui-react";
import { ChangeEvent, FormEvent, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

export default observer(function ActivityForm() {

    const { activityStore } = useStore();
    const { selectedActivity, closeForm, createActivity, updateActivity, loading } = activityStore;

    const initialState = selectedActivity ?? {
        id: '',
        title: '',
        category: '',
        description: '',
        date: '',
        city: '',
        venue: ''
    }

    const [activity, setActivity] = useState(initialState);

    function handleSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault();
        activity.id ? updateActivity(activity) : createActivity(activity);
    }

    function inputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target;
        setActivity({
            ...activity,
            [name]: value
        })
    }

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
                <Button onClick={closeForm} floated='right' type='button' content='Cancel' />
            </Form>
        </Segment>
    )
})