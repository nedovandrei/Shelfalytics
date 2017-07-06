export interface TableData {
    add?: TableAddButton;
    edit?: TableEditButton;
    delete?: any;
    columns: any;
}

export interface TableAddButton {
    addButtonContent?: string;
    createButtonContent?: string;
    cancelButtonContent?: string;
}

export interface TableEditButton {
    editButtonContent?: string;
    saveButtonContent?: string;
    cancelButtonContent?: string;
}

export interface TableDeleteButton {
    deleteButtonContent?: string;
    confirmDelete?: boolean;
}

export interface TableColumn {
    title: string;
    type: string;
}
