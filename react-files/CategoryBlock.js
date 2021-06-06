import React, {useState} from 'react';

function CategoryBlock({categories, handleAddCategory, handleEditCategory, handleDeleteCategory}) {
    
    const [showAddForm, setShowAddForm] = useState(true)
    const [editingCategory, setEditingCategory] = useState({})
    const [newCategory, setNewCategory] = useState("")


    const handleChange = function (evt) {
        setNewCategory(evt.target.value);
      };

    const onSubmit = (event) => {
        event.preventDefault()
        handleAddCategory(newCategory)
        event.target.reset();
      }

    return (
        <div id="page">
            <div id="categoryContainer">
                <div id="leftBox">
                    <h5>Category List</h5>
                    <table id="categoryTable">
                        <thead>
                            <tr>
                                <th>Category</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {categories.map((category) => (
                                <tr>
                                    <td>{category.categoryName}</td>
                                    <button class="astext" onClick={() => {handleDeleteCategory(category.categoryId)}}>X</button>
                                    |
                                    <button class="astext" onClick={() => {setShowAddForm(false);setEditingCategory(category)}}>Edit</button>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                <div id="rightBox">
                    {showAddForm && <div id="addForm">
                    <h5>Add Category</h5>
                        <form id="addCategoryForm" onSubmit={onSubmit}>
                            <label htmlFor="categoryName">Name: </label>
                            <input type="text" id="categoryName" onChange={handleChange}></input>
                            <button type="submit">Confirm</button>
                        </form>
                    </div>}
                    {!showAddForm && <div id="editForm">
                    <h5>Edit Category</h5>
                    <form id="editCategoryForm">
                            <label htmlFor="categoryName">Name: </label>
                            <input type="text" id="categoryName" placeholder={editingCategory.categoryName}></input>
                            <button type="" onClick={() => {setShowAddForm(true); handleEditCategory({categoryId : editingCategory.categoryId, categoryName:document.getElementById("categoryName").value})}}>Edit</button>
                        </form>
                    </div>}
                </div>
            </div>
        </div>
    )
}

export default CategoryBlock