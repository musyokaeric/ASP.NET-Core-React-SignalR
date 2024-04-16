import { Message } from "semantic-ui-react"

interface Props {
    errors: string[]
}

export default function ValidationError({ errors }: Props) {
    return (
        <Message negative>
            {
                errors &&
                <Message.List>
                    {
                        errors.map((error: string, index) => (
                            <Message.Item key={index}>{error}</Message.Item>
                        ))
                    }
                </Message.List>
            }
        </Message>
    )
}