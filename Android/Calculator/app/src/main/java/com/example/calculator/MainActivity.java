package com.example.calculator;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.databinding.BindingAdapter;

import org.mozilla.javascript.Context;
import org.mozilla.javascript.Scriptable;


public class MainActivity extends AppCompatActivity {

    TextView ResultView;
    boolean BracketsOpened = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ResultView = findViewById(R.id.ResultView);

        String[][] buttonsLabels = {
                { "C", "()", "%", "/" },
                { "7", "8", "9", "*" },
                { "4", "5", "6", "-" },
                { "1", "2", "3", "+" },
                { "+/-", "0", ",", "=" },
        };

        TableLayout baseLayout = findViewById(R.id.baseLayout);

        for (int i = 0; i < 5; i++) {
            TableRow row = new TableRow(this);
            for (int j = 0; j < 4; j++) {
                Button button = new Button(this);

                TableRow.LayoutParams params = new TableRow.LayoutParams();
                params.weight = 1;

                int margin = 5;
                params.setMargins(margin,margin, margin, margin);
                //params.height = (int)TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 100, getResources().getDisplayMetrics());
                params.width = 0;
                button.setLayoutParams(params);

                button.setText(buttonsLabels[i][j]);

                String text = button.getText().toString();
                switch (text) {
                    case "C": button.setOnClickListener(onClearClick); break;
                    case "=": button.setOnClickListener(onEqualClick); break;
                    case "+/-": break;
                    case "()": break;
                    case "%": break;

                    default:
                    {
                        if (IsMark(text.charAt(0)))
                            button.setOnClickListener(onMarkClick);
                        else button.setOnClickListener(onNumberClick);
                    }

                }
                row.addView(button);
            }

            baseLayout.addView(row);
        }


    }

    View.OnClickListener onNumberClick = v -> AddToTextView(((Button)v).getText());;
    View.OnClickListener onClearClick = v -> ResultView.setText("");
    View.OnClickListener onMarkClick = v -> {
        Button btn = (Button)v;
        AddToTextView(btn.getText());
    };
    View.OnClickListener onBracketsClick = v -> {
        ResultView.append(BracketsOpened ? "(" : ")");
        BracketsOpened = !BracketsOpened;

    };
    View.OnClickListener onEqualClick = v -> {

        Context context = Context.enter();
        context.setOptimizationLevel(-1); // this is required[2]
        Scriptable scope = context.initStandardObjects();

        Double result = (Double)context.evaluateString(scope, ResultView.getText().toString(), "<cmd>", 1, null);
        ResultView.setText(result.toString());

        //ScriptEngineManager manager = new ScriptEngineManager();


        /*
        ScriptEngineManager manager = new ScriptEngineManager();
        ScriptEngine engine = manager.getEngineByName("JavaScript");
        Object result = engine.eval(expression);
        */
    };

    private void AddToTextView(CharSequence text) {
        String textView = ResultView.getText().toString();

        if (textView.length() <= 1) {
            if (IsMark(text.charAt(0)) || text.charAt(0) == '0') return;
            else {
                ResultView.append(text);
                return;
            }
        }

        if (IsMark(textView.charAt(textView.length() - 1)) && IsMark(text.charAt(0)))
            ResultView.setText(textView.substring(0, textView.length() - 1));
        ResultView.append(text);


    }

    public boolean IsMark(char symbol) {
        String marks = "+-/*(),";
        return marks.contains(String.valueOf(symbol));
    }



    @BindingAdapter("{android:layout_height}")
    public static void setLayoutHeight(View view, float height) {

        Log.d("App", "Test");
        ViewGroup.LayoutParams layoutParams = view.getLayoutParams();
        layoutParams.height = (int)height;
        view.setLayoutParams(layoutParams);
    }
}