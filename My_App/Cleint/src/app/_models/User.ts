
import {Photo} from './photo';

export interface User {
id: number;
name: string;
knownAs: string;
age: number;
gender: string;
created: Date;
lastActive: Date;
photourl: string;
city: string;
coutnry: string;
interests?: string;
introduction?: string;
lookingfor?: string;
photos?: Photo[];

}
