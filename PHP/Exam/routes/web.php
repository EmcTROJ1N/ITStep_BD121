<?php

use App\Http\Controllers\ProfileController;
use Illuminate\Support\Facades\Route;
use App\Models\User;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "web" middleware group. Make something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});

Route::get('/dashboard', function () {
    return view('dashboard');
})->middleware(['auth', 'verified', 'role:1'])->name('dashboard');

Route::middleware('auth')->group(function () {
    Route::get('/profile', [ProfileController::class, 'edit'])->name('profile.edit');
    Route::patch('/profile', [ProfileController::class, 'update'])->name('profile.update');
    Route::delete('/profile', [ProfileController::class, 'destroy'])->name('profile.destroy');
});


Route::middleware(['auth', 'verified'])->group(function () {
    Route::get('/tasks', 'App\Http\Controllers\TasksController@index')->name("tasks");
});
Route::middleware(['auth', 'verified', 'role:1'])->group(function () {
    Route::get('/admin-panel', 'App\Http\Controllers\AdminPanelController@index')->name("admin-panel");
    Route::post('/add-task', 'App\Http\Controllers\AdminPanelController@AddTask')->name("add-task");
});

require __DIR__.'/auth.php';
