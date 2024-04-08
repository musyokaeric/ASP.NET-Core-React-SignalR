import Calendar from "react-calendar";
import { Header, Menu } from "semantic-ui-react";

export default function ActivityFilters() {
    return (
        <>
            <Menu vertical size='large' style={{ width: '100%', marginTop: 25 }}>
                <Menu.Item>
                    <Header icon='filter' color='teal' content='Filters' />
                </Menu.Item>

                <Menu.Item content='All Events' />
                <Menu.Item content="I'm going" />
                <Menu.Item content="I'm hosting" />
            </Menu>

            <Header icon='calendar' attached color='teal' content='Select date' />

            <Calendar />
        </>
    )
}