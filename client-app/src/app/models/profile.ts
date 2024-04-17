import { User } from "./user";

export interface IProfile {
    userName: string;
    displayName: string;
    image?: string;
    bio?: string;
    photos?: Photo[];
}

export class Profile implements IProfile {
    userName: string;
    displayName: string;
    image?: string;
    bio?: string;
    photos?: Photo[];

    constructor(user: User) {
        this.userName = user.username;
        this.displayName = user.displayName;
        this.image = user.image;
    }
}

export interface Photo {
    id: string;
    url: string;
    isMain: boolean;
}