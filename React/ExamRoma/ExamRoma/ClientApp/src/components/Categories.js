import {useEffect, useState} from "react";
import {FetchController} from "../FetchController";

export function Categories(props)
{
    const [categories, setCategories] = useState([]);
    let CategoriesController = new FetchController("api/Categories");
    
    useEffect(() =>
    {
        async function setCategoriesAsync()
        {
            let response = await CategoriesController.OnGetAsync();
            setCategories(response);
        }
        
        setCategoriesAsync();
    }, []);
    
    
    async function deleteCategory(id)
    {
        CategoriesController.OnDeleteAsync(id).then(response =>
            setCategories(categories.filter(category => category.id !== id)))
            .catch(err => alert(err))
    }
    
    async function createCategory(name)
    {
        CategoriesController.OnPostAsync({categoryName: name})
            .then(response => {
                setCategories([...categories, response]);
        }).catch(err => alert(err));
    }
    
    return (
        <div style={{
            display: 'flex',
        }}>
            <div style={{width: '60%'}}>
                <h1>Categories</h1>
                <table className="table table-striped" aria-labelledby="tableLabel">
                    <thead>
                        <tr>
                            <td>Category name</td>
                            <td>Action</td>
                        </tr>
                    </thead>
                    <tbody>

                    {categories.map(category =>
                        (
                            <tr>
                                <td>{category.categoryName}</td>
                                <td><button onClick={e => deleteCategory(category.id)}>Delete</button></td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>


            <div style={{width: '40%'}}>
                <form style={{marginLeft: '30px'}} name="addCategory">
                    <h1 style={{marginTop: '10px', marginBottom: '10px'}} >Add category</h1>
                    <input style={{marginTop: '10px', marginBottom: '10px'}} type="text" placeholder="Name" name="categoryName"/>
                    <br/>
                    <button style={{marginTop: '10px', marginBottom: '10px'}}
                            onClick={e => {
                                e.preventDefault();  
                                createCategory(document.addCategory.categoryName.value)
                                document.addCategory.categoryName.value = "";
                            }}>
                                Add
                    </button>
                </form>
            </div>
        </div>
    )
}
