@extends("layouts/app")

@section("header")
    <h1>Your tasks</h1>
    <p class="p-large">Here you can manage your tasks</p>
@endsection

@section("content")

    <div style="overflow-y: auto; max-width: 100vw">
        <table class="table table-bordered">
            <thead>
            <tr>
                <th>Body</th>
                <th>Created at</th>
                <th>Deadline</th>
            </tr>
            </thead>
            <tbody>
            @foreach($tasks as $task)
                <tr>
                    <td>{{ $task->title }} </td>
                    <td>{{ $task->created_at }}</td>
                    <td>{{ $task->updated_at }}</td>
                </tr>
            @endforeach
            </tbody>
        </table>
    </div>

    <div class="form-container" style="width: 300px">
        <h2>Delegate new task</h2>
        <form id="form" data-toggle="validator" data-focus="false" method="POST" action="{{ route("add-task") }}">
            @csrf
            <div class="form-group">
                <input type="text" class="form-control-input" id="ltext" name="user_id" required>
                <label class="label-control" for="ltext">Id</label>
                <div class="help-block with-errors"></div>
            </div>
            <div class="form-group">
                <input type="text" class="form-control-input" id="lemail" name="title" required>
                <label class="label-control" for="lemail">Task body</label>
                <div class="help-block with-errors"></div>
            </div>
            <div class="form-group">
                <input type="date" class="form-control-input" name="updated_at" id="lpassword" required>
                <label class="label-control" for="lpassword">Deadline</label>
                <div class="help-block with-errors"></div>
            </div>
            <input type="hidden" name="created_at" value="{{(new DateTime())->format('Y-m-d H:i:s') }}">
            <div class="form-group">
                <button type="submit" class="form-control-submit-button">Give task</button>
            </div>
        </form>
    </div>

@endsection
