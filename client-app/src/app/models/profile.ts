import { User } from "./user";

export interface IProfile {
    userName: string;
    displayName: string;
    image?: string;
    bio?: string;
}

export class Profile implements IProfile {
    userName: string;
    displayName: string;
    image?: string;
    bio?: string;

    constructor(user: User) {
        this.userName = user.username;
        this.displayName = user.displayName;
        this.image = user.image;
    }
}