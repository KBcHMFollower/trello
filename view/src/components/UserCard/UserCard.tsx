import React, { FC } from 'react'
import styles from './UserCard.module.scss'

type PropsType={
    userId:number;
    name:string;
    email:string;
    updateBoard: (data:{updateData:number; updateName:string;})=>void;
    type:'kick' | 'add'
}

export const UserCard:FC<PropsType> = ({userId, name, email, updateBoard, type}) => {

    const updateName = type==='kick' ? 'deleteId' : 'addId';

  return (
    <div className={styles.main_div}>
        <div className={styles.user_info_div}>
            <div>
                <h3>{name}</h3>
            </div>
            <div>
                <h2>{email}</h2>
            </div>
        </div>
        <button className={`${styles.button} ${type !== 'kick' ? styles.add : styles.kick}`}
        onClick={()=>updateBoard({updateName:updateName, updateData:userId})}
        >
            {type}
            </button>
    </div>
  )
}
