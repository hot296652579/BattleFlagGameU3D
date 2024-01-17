/*
 * @Author: super_javan 296652579@qq.com
 * @Date: 2024-01-11 21:46:49
 * @LastEditors: super_javan 296652579@qq.com
 * @LastEditTime: 2024-01-11 21:49:55
 * @FilePath: /BattleFlagGame20240103/Assets/Scripts/GGMgr.ts
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */
// 活动类型枚举
export enum GGEnum_Type {
    SingleGift = 'single_gift',
    MultipleGift = 'multiple_gift',
    PromptGift = 'dialogue_gift',
    FlipGameGift = 'flip_gift',
    ContinuousGift = 'continuous_charging_gift',
    FirstWeekGift = 'first_week_gift'
}

// 活动数据类
class Activity {
    public activity_type: GGEnum_Type;
    public activity_id: string;
    public trigger_Type?: string;
    public start_time?: string;
    public end_time?: string;
    public position?: string;
    public table_id?: number;
    public association_activity_id?: string;
    public thumbnail_detail?: {
        table_id?: number;
        id?: string;
        data?: string;
    };

    constructor(data: Activity) {
        this.activity_type = data.activity_type;
        this.activity_id = data.activity_id;
        this.trigger_Type = data.trigger_Type;
        this.start_time = data.start_time;
        this.end_time = data.end_time;
        this.position = data.position;
        this.table_id = data.table_id;
        this.association_activity_id = data.association_activity_id;
        this.thumbnail_detail = data.thumbnail_detail;
    }
}

class GGMgr {
    private static instance: GGMgr;
    public activities: Activity[] = [];
    public payActivities: Activity[] = [];
    private pendingData: Activity[] = [];

    private constructor() {
        // Private constructor to prevent instantiation
    }

    public static getInstance(): GGMgr {
        if (!GGMgr.instance) {
            GGMgr.instance = new GGMgr();
        }
        return GGMgr.instance;
    }

    public initData(data: Activity[]): void {
        this.activities = data.filter(activity => activity.activity_type !== GGEnum_Type.FirstWeekGift);
        this.payActivities = data.filter(activity => activity.activity_type === GGEnum_Type.FirstWeekGift);
    }

    public storeData(data: Activity): void {
        // 使用 activity_id 判断数据是否已存在，如果存在则更新，否则添加
        const index = this.pendingData.findIndex(item => item.activity_id === data.activity_id);
        if (index !== -1) {
            this.pendingData[index] = data;
        } else {
            this.pendingData.push(data);
        }
    }

    public checkAndSendData(): void {
        const currentDate: any = new Date();

        // 迭代复制 pendingData，避免在迭代过程中删除元素导致问题
        const dataToSend = [...this.pendingData];
        this.pendingData = [];

        dataToSend.forEach(data => {
            // 判断是否已经是 start_time 的第二天
            const startTime: any = new Date(data.start_time || 0);
            if (currentDate > startTime) {
                const daysDifference = Math.floor((currentDate - startTime) / (24 * 60 * 60 * 1000));
                if (daysDifference >= 1) {
                    // 已经是 start_time 的第二天，不发送数据
                    return;
                }
            }

            // 发送数据的逻辑，你可以在这里根据需求进行处理
            console.log(`Sending data:`, data);
        });
    }
}

// Example usage:
const activityData: Activity[] = [
    new Activity({
        activity_type: GGEnum_Type.SingleGift,
        activity_id: '1',
        start_time: '2024-01-11',
        end_time: '2024-01-15',
        table_id: 1,
        thumbnail_detail: {
            table_id: 1,
            id: 'thumbnail1',
            data: 'thumbnail_data1'
        }
    }),
    // Add more activity data...

    new Activity({
        activity_type: GGEnum_Type.FirstWeekGift,
        activity_id: '101',
        start_time: '2024-01-11',
        end_time: '2024-01-18',
        table_id: 2,
        thumbnail_detail: {
            table_id: 2,
            id: 'thumbnail2',
            data: 'thumbnail_data2'
        }
    }),
    // Add more pay activity data...
];

const ggMgr = GGMgr.getInstance();
ggMgr.initData(activityData);

console.log(ggMgr.activities);
console.log(ggMgr.payActivities);

// Example usage:
const ggMgr = GGMgr.getInstance();
const storeData1 = new Activity({ activity_type: GGEnum_Type.SingleGift, activity_id: '1', start_time: '2024-01-11' });
const storeData2 = new Activity({ activity_type: GGEnum_Type.FirstWeekGift, activity_id: '101', start_time: '2024-01-12' });

// 暂存数据
ggMgr.storeData(storeData1);
ggMgr.storeData(storeData2);

// 外部调用检查并发送数据
ggMgr.checkAndSendData();
