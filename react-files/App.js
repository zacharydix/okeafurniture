import './App.css';
import CategoryBlock from './components/CategoryBlock'
import React, {useState, useEffect } from 'react';

function App() {
  const [categories, setCategories] = useState([])

  useEffect(() => {
    var requestOptions = {
        method: 'GET',
        redirect: 'follow'
    };

    fetch("https://localhost:44370/api/categories/get/all", requestOptions)
        .then(response => {
            if (response.status >= 400) {
                return Promise.reject("Could not retrieve category data")
            }
            return response.json();
        })
        .then(result => setCategories(result))
        .catch(error => console.log('error', error));
  }, []);

  const handleAddCategory = (categoryName) => {
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
    "CategoryName": categoryName,
    });

    var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: raw,
    redirect: 'follow'
    };
  
    fetch("https://localhost:44370/api/categories/add", requestOptions)
            .then(response => {
                if (response.status >= 400) {
                    return Promise.reject("Could not add category data")
                }
                return response.json();
            })
            .then(result => setCategories(categories => [...categories,result]))
            .catch(error => console.log('error', error));
    }

    const handleEditCategory = ({categoryId, categoryName}) => {
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");

      var raw = JSON.stringify({
          "CategoryId": categoryId,
          "CategoryName": categoryName
      });

      var requestOptions = {
      method: 'PUT',
      headers: myHeaders,
      body: raw,
      redirect: 'follow'
      };

      fetch("https://localhost:44370/api/categories/edit", requestOptions)
          .then(response => {
              if (response.status >= 400) {
                  return Promise.reject("Could not edit category data")
              }
              return;
          })
          .then(result => setCategories(categories => {
            const otherCategories = categories.filter((category) => category.categoryId !== categoryId);
            const updatedCategories = [...otherCategories, {categoryId, categoryName}];
            return updatedCategories
          }))
          .catch(error => console.log('error', error));
  }

  const handleDeleteCategory = (categoryId) => {
      var requestOptions = {
      method: 'DELETE',
      redirect: 'follow'
      };

      fetch("https://localhost:44370/api/categories/delete/" + categoryId, requestOptions)
          .then(response => {
              if (response.status >= 400) {
                  return Promise.reject("Could not delete category data")
              }
              return;
          })
          .then(result => setCategories(categories => {
            return categories.filter((category) => category.categoryId !== categoryId);
          }))
          .catch(error => console.log('error', error));
  }

  return (
    <CategoryBlock categories={categories} handleAddCategory={handleAddCategory} handleEditCategory={handleEditCategory} handleDeleteCategory={handleDeleteCategory}></CategoryBlock>
  );
}

export default App;
