#include <optional>
#include <string>
#include <unordered_map>
#include <vector>

class Logger {
public:
  static void debug(std::string message);
  static void info(std::string message);
  static void warning(std::string message);
  static void error(std::string message);
};

struct MenuItemIngredient {
  std::string name;
  std::string type;
};

struct Decimal {};

struct MenuItem {
  std::string name;
  std::string description;
  int64_t imageId;
  Decimal price;
  std::string currency;
  Decimal grams;
  int64_t kcal;
  Decimal proteins;
  Decimal fats;
  Decimal carbohydrates;
  std::vector<MenuItemIngredient> ingredients;
};

struct MenuCategory {
  std::string name;
  std::vector<MenuItem> items;
};

struct Menu {
  int64_t pointId;
  std::string description;
  std::vector<MenuCategory> categories;
};

struct Point {
  int64_t id;
  int64_t version;
  int64_t latitude;
  int64_t longitude;
  std::unordered_map<std::string, std::string> tags;
};

struct Time {};

struct Coordinates {
  float latitude;
  float longitude;
};

struct SearchArea {
  Coordinates topLeft;
  Coordinates bottomRight;
};

struct PointsFilters {
  SearchArea searchArea;
  std::optional<std::string> searchQuery;
  std::optional<Time> visitTime;
  std::optional<std::vector<std::string>> cuisines;
  std::optional<std::vector<std::string>> ingredients;
  bool halalOnly;
  bool kosherOnly;
  bool leanOnly;
};

class PointsFiltersBuilder {
public:
  PointsFilters build();
  void setSearchArea(SearchArea searchArea);
  void setSearchQuery(std::string searchQuery);
  void setVisitTime(Time visitTime);
  void setCuisines(std::vector<std::string> cuisines);
  void setIngredients(std::vector<std::string> ingredients);
  void setHalalOnly();
  void setKosherOnly();
  void setLeanOnly();

private:
  std::optional<SearchArea> searchArea;
  std::optional<std::string> searchQuery;
  std::optional<Time> visitTime;
  std::optional<std::vector<std::string>> cuisines;
  std::optional<std::vector<std::string>> ingredients;
  bool halalOnly;
  bool kosherOnly;
  bool leanOnly;
};

class PointsRepository {
public:
  std::vector<Point> getPoints(PointsFilters filters);
  Point getPoint();
};

class MenusRepository {
public:
  Menu getMenu();
  void upsertMenu(Menu menu);
};

struct Image {
  int64_t id;
  std::vector<char> data;
};

class ImagesRepository {
public:
  Image getImage();
};

struct Date {};

struct Analytics {
  std::string name;
  Date startDate;
  Date endDate;
  std::vector<float> values;
};

class AnalyticsRepository {
public:
  Analytics getAnalytics();
};

class PointsSorter {
public:
  virtual void sort(std::vector<Point> points) = 0;
};

class NamePointsSorter : public PointsSorter {
public:
  void sort(std::vector<Point> points) override;
};

class ProximityPointsSorter : public PointsSorter {
public:
  void sort(std::vector<Point> points) override;
};

class DataFullnessPointsSorter : public PointsSorter {
public:
  void sort(std::vector<Point> points) override;
};

class RelevancePointsSorter : public PointsSorter {
public:
  void sort(std::vector<Point> points) override;
};

class PointsManager {
public:
  std::vector<Point>
  getPoints(PointsFilters filters,
            std::vector<std::shared_ptr<PointsSorter>> sorters);
  Point getPoint();

private:
  PointsRepository pointsRepository;
};

class MenuManager {
public:
  Menu getMenu();
  void upsertMenu(Menu menu);

private:
  MenusRepository menuRepository;
};

class ImagesManager {
public:
  Image getImage();

private:
  ImagesRepository imagesRepository;
};

class AnalyticsManager {
public:
  Analytics getAnalytics();

private:
  AnalyticsRepository analyticsRepository;
};

struct Request {};
struct Response {};

class ApiHandler {
public:
  virtual Response handleRequest(Request request) = 0;
};

class GetPointsHandler : public ApiHandler {
public:
  Response handleRequest(Request request) override;

private:
  PointsManager pointsManager;
};

class GetPointHandler : public ApiHandler {
public:
  Response handleRequest(Request request) override;

private:
  PointsManager pointsManager;
};

class GetMenuHandler : public ApiHandler {
public:
  Response handleRequest(Request request) override;

private:
  MenuManager menuManager;
};

class UpsertMenuHandler : public ApiHandler {
public:
  Response handleRequest(Request request) override;

private:
  MenuManager menuManager;
};

class GetImageHandler : public ApiHandler {
public:
  Response handleRequest(Request request) override;

private:
  ImagesManager imagesManager;
};

class GetAnalyticsHandler : public ApiHandler {
public:
  Response handleRequest(Request request) override;

private:
  AnalyticsManager analyticsManager;
};

class ApiManager {
public:
  Response handleRequest(Request request);

private:
  std::vector<std::shared_ptr<ApiHandler>> handlers;
};
