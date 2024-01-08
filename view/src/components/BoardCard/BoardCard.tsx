import React, { FC } from 'react'
import styles from './BoardCard.module.scss'
import { Link } from 'react-router-dom';

type PropsType = {
    userId:number;
    id: number;
    name: string;
    description: string;
    updateBoard: (data:{boardId:number; updateName:string; updateData:number})=>void;
}

export const BoardCard: FC<PropsType> = ({updateBoard, id, name, description, userId }) => {
    return (
        <div className={styles.main}>
            <Link to={`/boards/${id}`} className={`group`}>
                <div className={`${styles.text_div} group-hover:bg-blue-200 bg-orange-300`}>
                    <h2 className='break-words'>{name}</h2>
                </div>
                <div className={`${styles.text_div} group-hover:bg-blue-200 bg-slate-500`}>
                    <h3 className=' text-blue-500 font-semibold'>Description</h3>
                    <p className='break-words'>{description}</p>
                </div>
            </Link>
            <button className={styles.leave_button}
            onClick={()=>updateBoard({boardId:id, updateName:'deleteId', updateData:userId})}
            >Leave</button>
        </div>

    )
}
