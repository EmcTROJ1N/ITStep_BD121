@extends("layouts/app")

@section("header")
    <h1>Admin panel</h1>
    <p class="p-large">Here you can manage accounts or delegate tasks</p>
@endsection

@section("content")

    <div style="overflow-y: auto; max-width: 100vw">
        <table class="table table-bordered">
        <thead>
        <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Password</th>
            <th>Remember Token</th>
            <th>Created At</th>
            <th>Updated At</th>
        </tr>
        </thead>
        <tbody>
        @foreach($accounts as $account)
            <tr>
                <td>{{ $account->id }}</td>
                <td>{{ $account->name }}</td>
                <td>{{ $account->email }}</td>
                <td>{{ $account->password }}</td>
                <td>{{ $account->remember_token }}</td>
                <td>{{ $account->created_at }}</td>
                <td>{{ $account->updated_at }}</td>
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
