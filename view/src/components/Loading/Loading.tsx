import React from 'react'
import styles from './Loading.module.scss'

export const Loading = () => {
    return (
        <div>
            <div className={styles.loading_div}>
                LOADING...
            </div>
            <div className={styles.background}></div>
        </div>
    )
}
