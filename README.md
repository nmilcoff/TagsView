# TagsView

Simple and highly customizable Xamarin.iOS tag list view. Originally inspired by https://github.com/ElaWorkshop/TagListView

Customizable features:
- Left/Center/Right alignment
- Font
- X/Y Margins
- X/Y Paddings
- Text color
- Background text color
- Tag corner radius
- Tag border color
- Tag border width
- Tag border color
- Tag controls distance
- Tag icon
- Tag icon size
- Tag icon color

The widget also exposes two event handlers:
- Tag selected
- Tag button tapped

__Sample project__
<img src="Assets/sshot.png" alt="Screenshot" />

## Download

Get it on [Nuget](https://www.nuget.org/packages/TagsView/).

## Requirements

iOS >= 9.0

## Features and usage

It's super simple to get started:

```c#
public class ViewController : UIViewController
{
    private TagListView tagsView;

    public override void ViewDidLoad()
    {
        // code
        this.tagsView = new TagListView()
        {
            // you can customize properties here!
        };

        // you can attach a source object to each tag
        var myObject = new MyModel { Title = "I'm a MyModel!" };
        this.tagsView.AddTag(myObject.Title, myObject); 

        // but, if none is provided, it will be the text string 
        this.tagsView.AddTag("I'm a simple tag!"); 
    }
}
```

By default, `TagsView` will display a button next to the text of the tag. To prevent this, you can pass the value `false` for the parameter `enableTagButton` in its constructor.

As explained in the code snippet, each tag can contain a source object. These will be available for you on the events the control exposes:
- `TagSelected`: User tapped a tag.
- `TagButtonTapped`: User tapped a tag button.

There are also a lot of properties available for you to customize the look & feel:
Feature | Property | Type
--------- | ---- | ----
Alignment | Alignment | `TagsAlignment`
Text color | TagTextColor | `UIColor`
Tag background color | TagBackgroundColor | `UIColor`
Font | TextFont | `UIFont`
Tag corner radius | CornerRadius | `float`
Tag border width | BorderWidth | `float`
Tag border color | BorderColor | `UIColor`
Padding Y | PaddingY | `float`
Padding X | PaddingX | `float`
Margin Y | Margin Y | `float`
Margin X | Margin X | `float`
Distance between text and button | ControlsDistance | `float`
Button size | TagButtonSize | `float`
Button color | TagButtonColor | `UIColor`
Button icon | ButtonIcon | `UIImage`

## MvvmCross

WIP :)