<!-- Navigation -->
<nav class="navbar navbar-expand-lg navbar-dark navbar-custom fixed-top">
    <div class="container">
        <!-- Text Logo - Use this if you don't have a graphic logo -->
        <!-- <a class="navbar-brand logo-text page-scroll" href="index.html">Tivo</a> -->

        <!-- Mobile Menu Toggle Button -->
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault" aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-awesome fas fa-bars"></span>
            <span class="navbar-toggler-awesome fas fa-times"></span>
        </button>
        <!-- end of mobile menu toggle button -->

        <div class="collapse navbar-collapse" id="navbarsExampleDefault">
            <ul class="navbar-nav ml-auto">
                <li class="nav-item">
                    <a class="nav-link page-scroll" href="/">WELCOME <span class="sr-only">(current)</span></a>
                </li>

                @auth()
                    <li class="nav-item">
                        <a class="nav-link page-scroll" href="/tasks">SHOW MY TASKS</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link page-scroll" href="/admin-panel">ADMIN PANEL</a>
                    </li>
                @endauth
            </ul>
            <span class="nav-item">
                @guest()
                    <a class="btn-outline-sm" href="{{ route("login") }}">LOG IN</a>
                @endguest
                @auth()
                    <form method="POST" action="{{ route('logout') }}">
                            @csrf
                            <a class="btn-outline-sm" href="{{ route("logout")  }}"
                               onclick="event.preventDefault();
                                                this.closest('form').submit();">
                                {{ __('Log Out') }}
                            </a>
                        </form>
                @endauth
            </span>
        </div>
    </div> <!-- end of container -->
</nav> <!-- end of navbar -->
<!-- end of navigation -->
