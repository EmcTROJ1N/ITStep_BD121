<?php

namespace App\Http\Controllers;

use App\Models\Task;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
class AdminPanelController extends Controller
{
    public function index()
    {
        //$users = DB::select("select * from users");
        $users = User::all();
        return view("admin-panel")->with(["accounts" => $users]);
    }

    public function AddTask(Request $request)
    {
        $task = $request->all();
        $task["status"] = false;
        $task = Task::create($task);
        return redirect(route("admin-panel"));
    }
}
