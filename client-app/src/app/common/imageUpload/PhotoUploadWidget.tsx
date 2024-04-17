import { Grid, Header } from "semantic-ui-react";

export default function PhotoUploadWidget() {
    return (
        <Grid>
            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 1 - Add Photo' />
            </Grid.Column>

            <Grid.Column width={2} />

            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 2 - Resize Image' />
            </Grid.Column>

            <Grid.Column width={2} />

            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 3 - Preview & Upload' />
            </Grid.Column>
        </Grid>
    )
}