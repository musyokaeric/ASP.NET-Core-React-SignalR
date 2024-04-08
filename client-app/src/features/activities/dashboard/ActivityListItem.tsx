import { Link } from "react-router-dom";
import { Button, Item, Label } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import { useStore } from "../../../app/stores/store";
import { SyntheticEvent, useState } from "react";

interface Props {
    activity:Activity
}

export default function ActivityListItem({ activity }: Props) {
    const { activityStore } = useStore();
    const { deleteActivity, loading } = activityStore;

    const [target, setTarget] = useState('');

    // // sets the loading component to target the actual activity being deleted (delete button)
    function handleActivityDelete(event: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(event.currentTarget.name);
        deleteActivity(id);
    }

    return (
        <Item key={activity.id}>
            <Item.Content>
                <Item.Header as='a'>{activity.title}</Item.Header>

                <Item.Meta>{activity.date}</Item.Meta>

                <Item.Description>
                    <div>{activity.description}</div>
                    <div>{activity.city}, {activity.venue}</div>
                </Item.Description>

                <Item.Extra>
                    <Button as={Link} to={`/activities/${activity.id}`} floated='right' content='View' color='blue' />
                    <Button
                        name={activity.id}
                        loading={loading && target === activity.id} // sets the loading component to target the actual activity being deleted (delete button)
                        onClick={(event) => handleActivityDelete(event, activity.id)}
                        floated='right' content='Delete' color='red' />
                    <Label basic content={activity.category} />
                </Item.Extra>
            </Item.Content>
        </Item>
    )
}