import { DateParsingFlags } from 'ngx-bootstrap/chronos/create/parsing.types';

export interface Message {

    id: number;
    senderId: number;
    senderKnownAs: string;
    senderPhotoUrl: string;
    recipientId: number;
    recipientKnownAs: string;
    recipientPhotoUrl: string;
    content: string;
    isRead: boolean;
    dateRade: Date;
    messageSent: Date;


}