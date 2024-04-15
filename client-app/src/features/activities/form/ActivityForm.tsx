import { Button, Segment } from "semantic-ui-react";
import { useEffect, useState } from "react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { Link, useParams } from "react-router-dom";
import { Activity } from "../../../app/models/activity";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Formik, Form } from "formik";
import * as Yup from 'yup';
import MyTextInput from "../../../app/common/form/MyTextInput";
import MyTextArea from "../../../app/common/form/MyTextArea";
import MySelectInput from "../../../app/common/form/MySelectInput";
import { categoryOptions } from "../../../app/common/options/categoryOptions";
import MyDateInput from "../../../app/common/form/MyDateInput";

export default observer(function ActivityForm() {

    const { activityStore } = useStore();
    const { loading, loadActivity, loadingInitial } = activityStore;

    const { id } = useParams();

    const [activity, setActivity] = useState<Activity>({
        id: '',
        title: '',
        category: '',
        description: '',
        date: '',
        city: '',
        venue: ''
    })

    const validationSchema = Yup.object({
        title: Yup.string().required('The activity title is required'),
        description: Yup.string().required('The activity description is required'),
        category: Yup.string().required(),
        date: Yup.string().required(),
        city: Yup.string().required(),
        venue: Yup.string().required(),
    })

    useEffect(() => {
        if (id) loadActivity(id)
            .then(activity => setActivity(activity!));
    }, [id, loadActivity])

    //function handleSubmit(event: FormEvent<HTMLFormElement>) {
    //    event.preventDefault();

    //    if (!activity.id) {
    //        activity.id = uuid();
    //        createActivity(activity).then(() => navigate(`/activities/${activity.id}`));
    //    }
    //    else {
    //        updateActivity(activity).then(() => navigate(`/activities/${activity.id}`));
    //    }
    //}

    //function inputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
    //    const { name, value } = event.target;
    //    setActivity({
    //        ...activity,
    //        [name]: value
    //    })
    //}

    if (loadingInitial) return <LoadingComponent content='Loading activity...' />

    return (
        <Segment clearing>
            <Formik enableReinitialize initialValues={activity} onSubmit={values => console.log(values)} validationSchema={validationSchema}>
                {
                    ({ handleSubmit }) => (
                        <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                            <MyTextInput name='title' placeholder='Title' />

                            <MyTextArea rows={3} placeholder='Description' name='description' />
                            <MySelectInput options={categoryOptions} placeholder='Category' name='category' />
                            <MyDateInput
                                placeholderText='Date'
                                name='date'
                                showTimeSelect
                                timeCaption='time'
                                dateFormat='MMMM dd, yyyy h:mm aa' />
                            <MyTextInput placeholder='City' name='city' />
                            <MyTextInput placeholder='Venue' name='venue' />

                            <Button loading={loading} floated='right' positive type='submit' content='Submit' />
                            <Button as={Link} to={`/activities`} floated='right' type='button' content='Cancel' />
                        </Form>
                    )
                }
            </Formik>
        </Segment>
    )
})