import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import ActivityDetailedHeader from "./ActivityDetailedHeader";
import ActivityDetailedInfo from "./ActivityDetailedInfo";
import ActivityDetailedChat from "./ActivityDetailedChat";
import ActivityDetailedSideBar from "./ActivityDetailedSidebar";

export default observer(function ActivityDetails() {
    const { activityStore } = useStore();
    const { selectedActivity: activity, loadActivity, loadingInitial } = activityStore;

    const { id } = useParams();

    useEffect(() => {
        if (id) loadActivity(id);
    }, [id, loadActivity])

    if (loadingInitial || !activity) return <LoadingComponent content='' />;

    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityDetailedHeader activity={activity} />
                <ActivityDetailedInfo activity={activity} />
                <ActivityDetailedChat />
            </Grid.Column>

            <Grid.Column width={6}>
                <ActivityDetailedSideBar activity={activity} />
            </Grid.Column>
        </Grid>

        //<Card fluid>
        //    <Image src={`/assets/categoryImages/${activity.category}.jpg`} />

        //    <CardContent>
        //        <CardHeader>{activity.title}</CardHeader>

        //        <CardMeta>
        //            <span>{activity.date}</span>
        //        </CardMeta>

        //        <CardDescription>{activity.description}</CardDescription>
        //    </CardContent>

        //    <CardContent extra>
        //        <Button.Group widths='2'>
        //            <Button as={Link} to={ `/manage/${activity.id}`} basic color='blue' content='Edit' />
        //            <Button as={Link} to='/activities' basic color='grey' content='Cancel' />
        //        </Button.Group>
        //    </CardContent>
        //</Card>
    )
})