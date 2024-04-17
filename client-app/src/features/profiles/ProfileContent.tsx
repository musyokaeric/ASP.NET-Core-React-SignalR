import { Tab, TabPane } from "semantic-ui-react";

export default function ProfileContent() {
    const panes = [
        { menuItem: 'About', render: () => <TabPane>About Content</TabPane> },
        { menuItem: 'Photos', render: () => <TabPane>Photos Content</TabPane> },
        { menuItem: 'Events', render: () => <TabPane>Events Content</TabPane> },
        { menuItem: 'Followers', render: () => <TabPane>Followers Content</TabPane> },
        { menuItem: 'Following', render: () => <TabPane>Following Content</TabPane> },
    ]

    return (
        <Tab
            menu={{ fluid: true, vertical: true }}
            menuPosition='right'
            panes={panes}
        />
    )
}