# GiveCRM

## About GiveCRM

[GiveCRM](http://www.givecrm.org.uk/) was designed and developed at the inaugural [GiveCampUK](http://www.givecrm.org.uk/) in London, October 2011.  The aim of the project is to provide a Customer Relationship Management (CRM) system that has been explicitly designed with charities in mind, with a focus on simplicity of use.  

## Contributing to GiveCRM

If you want to contribute to the project, check out the list of open [Issues](https://github.com/givecampuk/givecrm/issues).  

You can:

 - raise an issue
 - suggest a feature for the application

If you would like to contribute code to the project:
 
  1. A bit of background reading:
    - [Setting up Git for Windows and connecting to GitHub](http://help.github.com/win-set-up-git/)
    - [The Simple Guide to Git](http://rogerdudler.github.com/git-guide/)
    - [How to GitHub: Fork, Branch, Track, Squash and Pull Request](http://gun.io/blog/how-to-github-fork-branch-and-pull-request/).
  2. Fork the repository ([how-to](http://help.github.com/fork-a-repo/))
  3. Make some changes to the code base
  4. Send us a Pull Request once you're happy with it ([how-to](http://help.github.com/send-pull-requests/))
   
We'll do a bit of a code review before accepting your patch.

### Git Flow

You will notice when you fork the GiveCRM repository that the default branch is `develop` rather than the more usual `master`.  We use the Git Flow branching model, [first described](http://nvie.com/posts/a-successful-git-branching-model/) by [nvie](http://www.twitter.com/nvie), so GiveCRM's `master` branch moves on only at specific points, when we're really sure we want to promote something to production.  

**Use of Git Flow is not required for contributing to GiveCRM**, particularly if you're submitting a bug-fix or small feature.  Its use is recommended for larger changes where `develop` might move on whilst you're completing your work.

#### Configuring Git Flow

There is a set of [helper scripts](https://github.com/nvie/gitflow) that will work on both Unix-based operating systems and Windows.  Follow the appropriate [installation instructions](https://github.com/nvie/gitflow/wiki/Installation) for your operating system, and configure your working copy repository for use with Git Flow by typing `git flow init`.  Accept all the default options to the questions that it asks you.

#### Using Git Flow

Pick a feature or bug to work on and create a new branch for that work by typing `git flow feature start <featurename>`.  This will create you a new *feature branch* for your work called `feature/<featurename>`, and you can use git as usual from this point.  

Once your feature is finished, type `git flow feature publish <featurename>`.  This will copy the *feature branch* to your `origin` repository on GitHub and you will then be able to submit a pull request to have it merged into GiveCRM's own `develop` branch.  **Note: do not use `git flow feature finish <featurename>`!**  This will automatically merge your *feature branch* back into `develop` and delete the *feature branch*, making it harder for you to submit your pull request.

If you wish to update your published feature branch after the initial publish, use a regular `git push origin feature/<featurename>`.  This will also update your pull request if you have one open for that branch.

If you find GiveCRM's `develop` branch has moved on, and you need/want to take advantage of the changes made there, you can update your feature branch as follows:

  1. Ensure you have a remote configured for the upstream repository.  You can use `git remote add upstream git://github.com/GiveCampUK/GiveCRM.git` to add it if it doesn't already exist.
  2. Type `git pull upstream develop:develop` to update your local repository with the upstream refs.
  3. Type `git flow feature rebase <featurename>` to rebase your feature branch on top of the new `develop`.
  
There is a lot of help available for Git Flow, which can be accessed by typing `git flow feature help`.

## Join the conversation

We're on Twitter ([@GiveCRM](http://www.twitter.com/givecrm/)) using the hashtag #GiveCRM.  We have a [Trello board](https://trello.com/b/gGG1duEF) for creating and detailing tasks, and a [JabbR chatroom](http://jabbr.net/#/rooms/givecrm); why not pop in and say hi?