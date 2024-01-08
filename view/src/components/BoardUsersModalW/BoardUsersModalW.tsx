import React, { FC, useState } from 'react'
import styles from './BoardUsersModalW.module.scss'
import { UserCard } from '../UserCard/UserCard';
import { IUser } from '../../Api/models/user-model';
import { userEndpoints } from '../../Api/api-endpoints/user-endpoints';


type PropsType = {
    updateBoard:(data:{updateName:string; updateData:number})=>void;
    users:IUser[];
}

export const BoardUsersModalW:FC<PropsType> = ({updateBoard, users}) => {

    const [isOpen, setIsOpen] = useState(false);
    const [isAdding, setIsAdding] = useState(false);

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');

    const [email, setEmail] = useState('');

    const {data: findUsers, isLoading, error} = userEndpoints.useGetAllUsersQuery(email);
    const isFindLoading = !findUsers || isLoading;

    return (
        <>
        <button
        onClick={()=>setIsOpen(!isOpen)} 
        className={styles.open_button}>
            USERS
        </button>

        <div className={`${styles.modal} ${isOpen && styles.active}`}>
            <div>
            <h2 className='font-bold'>Subscribers</h2>
            <button
            onClick={()=>setIsAdding(!isAdding)}
             className={styles.create_button}>Add User</button>
             <div className={`${styles.add_field} ${isAdding && styles.active}`}>
                <input className={styles.input} type="email" placeholder='Email' 
                value={email}
                onChange={e=>setEmail(e.target.value)}/>
                {isFindLoading ? (
                    <>
                    Loading...
                    </>
                ) : (
                    <>
                    {findUsers.filter(e=>!users.includes(e)).map(e=><UserCard key={e.id} type='add' userId={e.id} name={e.name} email={e.email} updateBoard={updateBoard}/>)}
                    </>
                )}
             </div>
            </div>
            <div className={styles.users_list_div}>
                {users.map(e=><UserCard key={e.id} type='kick' userId={e.id} name={e.name} email={e.email} updateBoard={updateBoard}/>)}
            </div>
        </div>
        <div
        onClick={()=>setIsOpen(!isOpen)}
         className={`${styles.overlay} ${isOpen && styles.active}`}></div>
        </>
        
    )
}
